using AssetRipper.Parser.Classes.Misc;
using AssetRipper.Parser.Classes.Misc.Serializable;
using AssetRipper.Parser.Classes.Utils.Extensions;
using AssetRipper.Parser.Files;
using AssetRipper.IO.Asset;

namespace AssetRipper.Parser.Classes.AnimationClip
{
	public struct HumanPose : IAssetReadable
	{
		/// <summary>
		/// 5.4.0 and greater
		/// </summary>
		public static bool IsVector3(Version version) => version.IsGreaterEqual(5, 4);
		/// <summary>
		/// 5.2.0 and greater
		/// </summary>
		public static bool HasTArray(Version version) => version.IsGreaterEqual(5, 2);

		public void Read(AssetReader reader)
		{
			RootX.Read(reader);
			if (IsVector3(reader.Version))
			{
				LookAtPosition = reader.ReadAsset<Vector3f>();
			}
			else
			{
				LookAtPosition.Read(reader);
			}
			LookAtWeight.Read(reader);
			GoalArray = reader.ReadAssetArray<HumanGoal>();
			LeftHandPose.Read(reader);
			RightHandPose.Read(reader);
			DoFArray = reader.ReadSingleArray();

			if (HasTArray(reader.Version))
			{
				if (IsVector3(reader.Version))
				{
					TDoFArray = reader.ReadVector3Array();
				}
				else
				{
					TDoFArray = reader.ReadAssetArray<Vector4f>();
				}
			}
		}

		public HumanGoal[] GoalArray { get; set; }
		public float[] DoFArray { get; set; }
		public Vector4f[] TDoFArray { get; set; }

		public XForm RootX;
		public Vector4f LookAtPosition;
		public Vector4f LookAtWeight;
		public HandPose LeftHandPose;
		public HandPose RightHandPose;
	}
}
