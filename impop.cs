using System;
using System.Diagnostics;
using System.IO;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.Exceptions;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace dwglinkopen
{
	// Token: 0x02000004 RID: 4
	[Transaction(TransactionMode.Manual)]
	public class impop : IExternalCommand
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002224 File Offset: 0x00000424
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			UIApplication application = commandData.Application;
			Document document = application.ActiveUIDocument.Document;
			UIDocument activeUIDocument = application.ActiveUIDocument;
			ISelectionFilter selectionFilter = new ImportFilter();
			try
			{
				Reference reference = activeUIDocument.Selection.PickObject(ObjectType.Element, selectionFilter, "Pick AutoCAD Link:");
				ImportInstance importInstance = document.GetElement(reference.ElementId) as ImportInstance;
				CADLinkType cadlinkType = document.GetElement(importInstance.GetTypeId()) as CADLinkType;
				ExternalFileReference externalFileReference = cadlinkType.GetExternalFileReference();
				string text = ModelPathUtils.ConvertModelPathToUserVisiblePath(externalFileReference.GetAbsolutePath());
				if (!File.Exists(text))
				{
					TaskDialog.Show("Path Inaccesible", "Drawing not found. Make sure it's not on a local storage.");
					return Result.Failed;
				}
				new Process
				{
					StartInfo = 
					{
						UseShellExecute = true,
						FileName = text
					}
				}.Start();
			}
			catch (Autodesk.Revit.Exceptions.OperationCanceledException)
			{
				return Result.Cancelled;
			}
			return Result.Succeeded;
		}
	}
}
