using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace dwglinkopen
{
	// Token: 0x02000003 RID: 3
	public class ImportFilter : ISelectionFilter
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000021D0 File Offset: 0x000003D0
		public bool AllowElement(Element element)
		{
			ImportInstance importInstance = element as ImportInstance;
			if (importInstance != null)
			{
				if (importInstance.IsLinked)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002208 File Offset: 0x00000408
		public bool AllowReference(Reference refer, XYZ point)
		{
			return false;
		}
	}
}
