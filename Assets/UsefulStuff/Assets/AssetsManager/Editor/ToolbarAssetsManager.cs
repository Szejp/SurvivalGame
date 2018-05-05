using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;

namespace GDExtensions.Toolbar {
    
    public class ToolbarAssetsManager : EditorWindow {
		private float mediumButtonsWidth = 150f;
		private float smallButtonWidth = 100f;

        [MenuItem("Window/Assets Manager")]

        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(ToolbarAssetsManager));
        }

        AssetsManagerController Controller {
			get {
				if (controller == null) return controller = new AssetsManagerController();
				return controller;
			}
			set {
				controller = value;
			}
		}
		AssetsManagerController controller;

		Vector2 scrollPos = Vector2.zero;


		[InitializeOnLoadMethod]
		static void Init() {}		

		List<AssetModel> list;

		private void OnGUI() {
			scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUIStyle.none, GUI.skin.verticalScrollbar);

			if (GUILayout.Button("Check unused assets")) {
				list = Controller.GetUnusedAssets();
			}

			GUILayout.Label("Unused assets");

			#region Sorting buttons
			GUILayout.BeginHorizontal();
			GUILayout.Label("Sorting", GUILayout.Width(smallButtonWidth));
			if (GUILayout.Button("By folder", GUILayout.Width(smallButtonWidth))) {
				Controller.SortUnusedAssetsListByFolder();
			}
			if (GUILayout.Button("By type", GUILayout.Width(smallButtonWidth))) {
				Controller.SortUnusedAssetsListByType();
			}
			if (GUILayout.Button("By size", GUILayout.Width(smallButtonWidth))) {
				Controller.SortUnusedAssetsListBySize();
			}
			GUILayout.EndHorizontal();
			#endregion

			//var list = Controller.GetUnusedAssets().ToList();

			if (list != null) {
				foreach (var a in list) {
					#region Assets Row
					GUILayout.BeginHorizontal(GUILayout.Width(400f));

					if (GUILayout.Button("delete", GUILayout.Width(smallButtonWidth))) {
						Controller.RemoveAsset(a.path);
					}

					a.ToBeRemoved = GUILayout.Toggle(a.ToBeRemoved, "");
					var obj = AssetDatabase.LoadAssetAtPath(a.path, typeof(Object));

					EditorGUILayout.LabelField(a.FolderName, GUILayout.MaxWidth(100f));
					EditorGUILayout.ObjectField(obj, typeof(Object), false, GUILayout.MinWidth(200f));
					EditorGUILayout.LabelField(FileSizeConverter.AdjustSize(a.FileSize), GUILayout.Width(100f));

					GUILayout.EndHorizontal();
					#endregion
				}
			}


			GUILayout.FlexibleSpace();

			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Select all", GUILayout.Width(mediumButtonsWidth))) {
				Controller.SelectAll(list);
			}

			GUILayout.FlexibleSpace();

			if (GUILayout.Button("Delete selected", GUILayout.Width(mediumButtonsWidth))) {
				Controller.RemoveSelected();
			}
			GUILayout.EndHorizontal();

			EditorGUILayout.EndScrollView();
		}
	}

	public class AssetsManagerController {
		bool isSizeDescending = false;
		bool isTypeDescending = false;
		bool isFolderDescending = false;

		AssetsManagerModel Model {
			get {
				if (model == null) return model = new AssetsManagerModel();
				return model;
			}
			set {
				model = value;
			}
		}
		AssetsManagerModel model;

		public void SortUnusedAssetsListBySize() {
			AssetModelListSorter.SortBySize(Model.unusedAssets, isSizeDescending);
			isSizeDescending = !isSizeDescending;
		}

		public void SortUnusedAssetsListByType() {
			AssetModelListSorter.SortByType(Model.unusedAssets, isTypeDescending);
			isTypeDescending = !isTypeDescending;
		}

		public void SortUnusedAssetsListByFolder() {
			AssetModelListSorter.SortByFolder(Model.unusedAssets, isFolderDescending);
			isFolderDescending = !isFolderDescending;
		}

		public void CheckUnusedAssets() {
			Model.CheckUnusedAssets();
		}

		public void RemoveAsset(string path) {
			Model.RemoveAsset(path);
		}

		public List<AssetModel> GetUnusedAssets() {
			return Model.GetUnusedAssets();
		}

		public void RemoveSelected() {
			Model.RemoveSelected();
		}

		public void SelectAll(List<AssetModel> list) {
			if (list != null && list.Count > 0)
				list.ForEach(p => p.ToBeRemoved = true);
		}
	}

	public class AssetsManagerModel {
		public List<AssetModel> unusedAssets;

		string[] specialFiles = { ".unity", "/Editor", "/Resources", "/StreamingAssets", "/Plugins", ".cs" };

		public string[] GetAllAssetPaths() {

			return AssetDatabase.GetAllAssetPaths().Where(p => p.StartsWith("Assets/")).ToArray();
		}

		public void CheckUnusedAssets() {
			unusedAssets = new List<AssetModel>();
			var paths = GetAllAssetPaths();
			var dep = AssetDatabase.GetDependencies(paths, false);
			var result = paths.Where(p => !dep.Contains(p)).Where(q => q.Contains(".")).Where(q => !IsSpecialFilePath(q)).ToArray();

			foreach (var p in result) {
				unusedAssets.Add(new AssetModel { path = p });
			}
		}

		public List<AssetModel> GetUnusedAssets() {
			CheckUnusedAssets();
			return unusedAssets;
		}


		public void RemoveAsset(string path) {
			unusedAssets.Remove(unusedAssets.Single(q => q.path == path));
			AssetDatabase.DeleteAsset(path);
		}

		public void RemoveSelected() {
			unusedAssets.ToList().ForEach(p => {
				if (p.ToBeRemoved) {
					RemoveAsset(p.path);
				};
			});
		}


		private bool IsSpecialFilePath(string path) {
			foreach (string s in specialFiles) {
				if (path.Contains(s)) {
					return true;
				}
			}

			return false;
		}
	}

	public class AssetModel {
		public string path { get; set; }
		public string Name {
			get {
				return new string(path.Skip(path.LastIndexOf('/')).ToArray());
			}
		}

		public string GUID {
			get {
				return AssetDatabase.AssetPathToGUID(path);
			}
		}

		public long FileSize {
			get {
				if (File.Exists(path)) return new FileInfo(path).Length;
				return 0;
			}
		}

		public bool ToBeRemoved = false;

		public string FolderName {
			get {
				var strings = path.Split('/');
				return strings[strings.Count() - 2];
			}
		}
	}

	public class FolderRemover {
		private static bool dryRun = true;

		public static void RemoveEmptyFoldersMenuItem() {
			var index = Application.dataPath.IndexOf("/Assets");
			var projectSubfolders = Directory.GetDirectories(Application.dataPath, "*", SearchOption.AllDirectories);

			// Create a list of all the empty subfolders under Assets.
			var emptyFolders = projectSubfolders.Where(path => IsEmptyRecursive(path)).ToArray();

			foreach (var folder in emptyFolders) {
				// Verify that the folder exists (may have been already removed).
				if (Directory.Exists(folder)) {
					Debug.Log("Deleting : " + folder);

					if (!dryRun) {
						// Remove dir (recursively)
						Directory.Delete(folder, true);

						// Sync AssetDatabase with the delete operation.
						AssetDatabase.DeleteAsset(folder.Substring(index + 1));
					}
				}
			}

			// Refresh the asset database once we're done.
			AssetDatabase.Refresh();
		}


		private static bool IsEmptyRecursive(string path) {
			// A folder is empty if it (and all its subdirs) have no files (ignore .meta files)
			return Directory.GetFiles(path).Select(file => !file.EndsWith(".meta")).Count() == 0
				&& Directory.GetDirectories(path, string.Empty, SearchOption.AllDirectories).All(IsEmptyRecursive);
		}
	}

	public class FileSizeConverter {
		public static string AdjustSize(long len) {
			string[] sizes = { "B", "KB", "MB", "GB", "TB" };
			int order = 0;
			while (len >= 1024 && order < sizes.Length - 1) {
				order++;
				len = len / 1024;
			}

			// Adjust the format string to your preferences. For example "{0:0.#}{1}" would
			// show a single decimal place, and no space.
			return System.String.Format("{0:0.##} {1}", len, sizes[order]);
		}
	}

	public static class AssetModelListSorter {
		public static void SortBySize(List<AssetModel> list, bool isDescending) {
			if (list == null || list.Count < 2) return;

			list.Sort((a, b) => {
				if (isDescending) return b.FileSize.CompareTo(a.FileSize);
				return a.FileSize.CompareTo(b.FileSize);
			});
		}

		public static void SortByFolder(List<AssetModel> list, bool isDescending) {
			if (list == null || list.Count < 2) return;

			list.Sort((a, b) => {
				if (isDescending) return b.FolderName.CompareTo(a.FolderName);
				return a.FolderName.CompareTo(b.FolderName);
			});
		}

		public static void SortByType(List<AssetModel> list, bool isDescending) {
			if (list == null || list.Count < 2) return;

			list.Sort((a, b) => {
				if (a.path == null || b.path == null) return 1;
				var pathA = GetTypeFromPath(a.path);
				var pathB = GetTypeFromPath(b.path);

				if (isDescending) return pathB.CompareTo(pathA);
				return pathA.CompareTo(pathB);
			});
		}


		private static string GetTypeFromPath(string path) {
			if (path == null || !path.Contains('.')) return null;
			return new string(path.Skip(path.LastIndexOf('.')).ToArray());
		}
	}
}