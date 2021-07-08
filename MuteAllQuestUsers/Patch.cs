using System;
using System.Reflection;
using Harmony;

namespace MuteAllQuestUsers
{
	// Token: 0x0200000C RID: 12
	public class Patch
	{
		public Patch(Type PatchClass, Type YourClass, string Method, string ReplaceMethod, BindingFlags stat = BindingFlags.Static, BindingFlags pub = BindingFlags.NonPublic)
		{
			Patch.HInstance.Patch(AccessTools.Method(PatchClass, Method, null, null), this.GetPatch(YourClass, ReplaceMethod, stat, pub), null, null);
		}

		private HarmonyMethod GetPatch(Type YourClass, string MethodName, BindingFlags stat, BindingFlags pub)
		{
			return new HarmonyMethod(YourClass.GetMethod(MethodName, stat | pub));
		}

		// Token: 0x04000030 RID: 48
		private static readonly HarmonyInstance HInstance = HarmonyInstance.Create("DripPatches");

		internal static Type GetPatch(string v)
		{
			throw new NotImplementedException();
		}
	}
}
