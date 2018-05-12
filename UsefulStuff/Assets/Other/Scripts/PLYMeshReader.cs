using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


public class PLYMeshReader {
	public static PLYMeshResult ReadBinary(string filepath) {
		int vertexCount = 0, facesCount = 0, vertexPropertiesCount = 0, counter = 0, indexCount = 0;
		List<float> vertices = new List<float>();
		List<int> faces = new List<int>();
		bool endHeader = false;

		using (StreamReader reader = new StreamReader(filepath)) {
			string inputLine = "";
			while (!endHeader) {
				inputLine = reader.ReadLine().Trim();
				if (inputLine.Length > 0) {
					if (inputLine.Contains("element vertex")) {
						vertexCount = Int32.Parse(inputLine.Replace("element vertex ", ""));
						counter = 0;
					}
					else if (inputLine.Contains("element face")) {
						vertexPropertiesCount = counter;
						facesCount = Int32.Parse(inputLine.Replace("element face ", ""));
						counter = 0;
					}
					else if (inputLine.Contains("end_header")) {
						endHeader = true;
						counter = 0;
					}
					else {
						counter++;
					}
				}
			}
		}

		var bytes = File.ReadAllBytes(filepath);
		byte[] searchString = Encoding.ASCII.GetBytes("end_header");
		int offset = searchString.Length;
		for (int i = 0; i < bytes.Length; ++i) {
			if (ContainsBytes(bytes, i, searchString)) {
				offset += i + 1;
				break;
			}
		}

		int vertexFloatCount = vertexCount * vertexPropertiesCount; // 6 bo 6 propertiesow (pozycja + normal)
		float[] vertexBufferData = new float[vertexFloatCount];
		int vertexBytesLength = vertexFloatCount * 4; // 4 bo float ma 4 bajty
		Buffer.BlockCopy(bytes, offset, vertexBufferData, 0, vertexBytesLength);

		for (int i = 0; i < vertexFloatCount; i += vertexPropertiesCount) {
			vertices.Add(vertexBufferData[i]);
			vertices.Add(vertexBufferData[i + 1]);
			vertices.Add(vertexBufferData[i + 2]);
		}

		offset += vertexBytesLength;

		int[] indices = new int[255];
		for (int i = 0; i < facesCount; i++) {
			indexCount = bytes[offset];
			offset++;
			Buffer.BlockCopy(bytes, offset, indices, 0, indexCount * 4); // 4 bo int ma 4 bajty
			offset += indexCount * 4;

			// przyjmuje ze trojkaty
			faces.Add(indices[0]);
			faces.Add(indices[1]);
			faces.Add(indices[2]);
		}

		return new PLYMeshResult {
			vertices = vertices.ToArray(),
			faces = faces.ToArray()
		};
	}

	static bool ContainsBytes(byte[] bytes, int index, byte[] searchedBytes) {
		for (int i = 0; i < searchedBytes.Length; ++i) {
			if (bytes[index + i] != searchedBytes[i])
				return false;
		}
		return true;
	}
}

public class PLYMeshResult {
	public float[] vertices;
	public int[] faces;
}
