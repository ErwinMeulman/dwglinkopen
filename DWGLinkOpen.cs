using System;
using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.Exceptions;
using Autodesk.Revit.UI;

namespace dwglinkopen
{
	// Token: 0x02000002 RID: 2
	public class DWGLinkOpen : IExternalApplication
	{
		// Token: 0x06000001 RID: 1
		public Result OnStartup(UIControlledApplication application)
		{
			try
			{
				application.CreateRibbonTab(DWGLinkOpen.tabName);
			}
			catch (Autodesk.Revit.Exceptions.ArgumentException)
			{
			}
			int num = 0;
			RibbonPanel ribbonPanel = null;
			foreach (RibbonPanel ribbonPanel2 in application.GetRibbonPanels(DWGLinkOpen.tabName))
			{
				if (ribbonPanel2.Name == "Interoperability Tools")
				{
					ribbonPanel = ribbonPanel2;
					num = 1;
				}
			}
			if (num == 0)
			{
				ribbonPanel = application.CreateRibbonPanel(DWGLinkOpen.tabName, "Interoperability Tools");
			}
			string location = Assembly.GetExecutingAssembly().Location;
			PushButtonData itemData = new PushButtonData("cmdimpop", "Open\nDWG Link", location, "dwglinkopen.impop");
			PushButton pushButton = ribbonPanel.AddItem(itemData) as PushButton;
			pushButton.ToolTip = "Opens a DWG link in a new or already running AutoCAD instance (or any other DWG viewer/editor set as the default DWG handler).";
			ContextualHelp contextualHelp = new ContextualHelp(ContextualHelpType.Url, "help.html");
			pushButton.SetContextualHelp(contextualHelp);
			BitmapImage largeImage = new BitmapImage(new Uri("dwglop.png"));
			pushButton.LargeImage = largeImage;
			return Result.Succeeded;
		}

		// Token: 0x06000002 RID: 2
		public Result OnShutdown(UIControlledApplication application)
		{
			return Result.Succeeded;
		}

		// Token: 0x04000001 RID: 1
		public static string tabName = "Bird Tools";
	}
}
