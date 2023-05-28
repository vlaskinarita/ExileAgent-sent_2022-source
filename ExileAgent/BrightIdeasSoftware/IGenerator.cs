using System;
using System.Collections.Generic;

namespace BrightIdeasSoftware
{
	public interface IGenerator
	{
		void GenerateAndReplaceColumns(ObjectListView olv, Type type, bool allProperties);

		IList<OLVColumn> GenerateColumns(Type type, bool allProperties);
	}
}
