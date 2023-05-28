using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ns12;
using PoEv2;

namespace ns25
{
	internal sealed class Class135 : Class129
	{
		public Class135(MainForm mainForm_1) : base(mainForm_1)
		{
		}

		[DebuggerStepThrough]
		protected override void vmethod_0()
		{
			Class135.Class144 @class = new Class135.Class144();
			@class.class135_0 = this;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<Class135.Class144>(ref @class);
		}
	}
}
