using System;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Game.Tiles;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Game.Managers;
using Sirenix.OdinInspector.Editor;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game
{
	[ExecuteAlways]
	public class OverlapWFC : MonoBehaviour
	{
		public Training training = null;
		public int gridsize = 1;
		public int width = 20;
		public int height = 20;

		public int seed = 0;

		//[HideInInspector]
		public int N = 2;
		public bool periodicInput = false;
		public bool periodicOutput = false;
		public int symmetry = 1;
		public int foundation = 0;
		public int iterations = 0;
		public bool incremental = false;
		public OverlappingModel model = null;
		public GameObject[,] rendering;
		public List<TileBase> Tiles;
		public GameObject output;
		private Transform group;
		private bool undrawn = true;

		public static bool IsPrefabRef(UnityEngine.Object o)
		{
#if UNITY_EDITOR
			return PrefabUtility.GetOutermostPrefabInstanceRoot(o) != null;
#else
		return true;
#endif
		}

		static GameObject CreatePrefab(UnityEngine.Object fab, Vector3 pos, Quaternion rot)
		{
#if UNITY_EDITOR
			GameObject e = PrefabUtility.InstantiatePrefab(fab as GameObject) as GameObject;
			e.transform.position = pos;
			e.transform.rotation = rot;
			return e;
#else
		GameObject o = GameObject.Instantiate(fab as GameObject) as GameObject; 
		o.transform.position = pos;
		o.transform.rotation = rot;
		return o;
#endif
		}

		public void Clear()
		{
			if (group != null)
			{
				if (Application.isPlaying)
				{
					Destroy(group.gameObject);
				}
				else
				{
					DestroyImmediate(group.gameObject);
				}

				group = null;
			}
		}

		void Start()
		{
			/*Generate();
			Run();*/
		}

		void Update()
		{
			if (incremental)
			{
				//Run();
			}
		}

		public void SetGridData(GridData data)
		{
			width = data.GridSize.x;
			height = data.GridSize.y;
		}

		public void Generate()
		{
			if (training == null)
			{
				Debug.Log("Can't Generate: no designated Training component");
			}

			if (IsPrefabRef(training.gameObject))
			{
				GameObject o = CreatePrefab(training.gameObject, new Vector3(0, 99999f, 0f), Quaternion.identity);
				training = o.GetComponent<Training>();
			}

			if (training.sample == null)
			{
				training.Compile();
			}

			if (output == null)
			{
				Transform ot = transform.Find("output-overlap");
				if (ot != null)
				{
					output = ot.gameObject;
				}
			}

			if (output == null)
			{
				output = new GameObject("output-overlap");
				output.transform.parent = transform;
				output.transform.position = this.gameObject.transform.position;
				output.transform.rotation = this.gameObject.transform.rotation;
			}

			for (int i = 0; i < output.transform.childCount; i++)
			{
				GameObject go = output.transform.GetChild(i).gameObject;
				if (Application.isPlaying)
				{
					Destroy(go);
				}
				else
				{
					DestroyImmediate(go);
				}
			}

			group = new GameObject(training.gameObject.name).transform;
			group.parent = output.transform;
			group.position = output.transform.position;
			group.rotation = output.transform.rotation;
			group.localScale = new Vector3(1f, 1f, 1f);
			rendering = new GameObject[width, height];
			model = new OverlappingModel(training.sample, N, width, height, periodicInput, periodicOutput, symmetry,
				foundation);
			undrawn = true;
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.cyan;
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.DrawWireCube(
				new Vector3(width * gridsize / 2f - gridsize * 0.5f, height * gridsize / 2f - gridsize * 0.5f, 0f),
				new Vector3(width * gridsize, height * gridsize, gridsize));
		}

		public void Run()
		{
			if (model == null)
			{
				return;
			}

			if (undrawn == false)
			{
				return;
			}

			if (model.Run(seed, iterations))
			{
				Draw();
			}
		}

		public GameObject GetTile(int x, int y)
		{
			return rendering[x, y];
		}

		public void Draw()
		{
			Tiles.Clear();
			Tiles = new List<TileBase>();

			if (output == null)
			{
				return;
			}

			if (group == null)
			{
				return;
			}

			undrawn = false;
			try
			{
				for (int y = 0; y < height; y++)
				{
					for (int x = 0; x < width; x++)
					{
						if (rendering[x, y] == null)
						{
							int v = (int) model.Sample(x, y);
							if (v != 99 && v < training.tiles.Length)
							{
								Vector3 pos = new Vector3(x * gridsize, y * gridsize, 0f);
								int rot = (int) training.RS[v];
								GameObject fab = training.tiles[v] as GameObject;
								if (fab != null)
								{
									GameObject tile = (GameObject) Instantiate(fab, new Vector3(), Quaternion.identity);
									Vector3 fscale = tile.transform.localScale;
									tile.transform.parent = group;
									tile.transform.localPosition = pos;
									tile.transform.localEulerAngles = new Vector3(0, 0, 360 - (rot * 90));
									tile.transform.localScale = fscale;
									rendering[x, y] = tile;
									var tileBase = tile.GetComponent<TileBase>();
									tile.gameObject.name = tileBase.GetType().Name + "_" + x + "_" + y;
									tileBase.SetTilePosId(x, y);
									Tiles.Add(tileBase);
									tileBase.Init(new TileBase.TileCoords {Pos = new Vector2Int(x, y)});
								}
							}
							else
							{
								undrawn = true;
							}
						}
					}
				}
			}
			catch (IndexOutOfRangeException)
			{
				model = null;
				return;
			}
		}
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(OverlapWFC))]
	public class WFCGeneratorEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			OverlapWFC generator = (OverlapWFC) target;
			if (generator.training != null)
			{
				if (GUILayout.Button("generate"))
				{
					generator.Generate();
				}

				if (generator.model != null)
				{
					if (GUILayout.Button("RUN"))
					{
						generator.Run();
					}
				}
				
				if (GUILayout.Button("quick generate"))
				{
					generator.Generate();
					generator.Run();
				}
			}

			DrawDefaultInspector();
		}
	}
#endif

}
