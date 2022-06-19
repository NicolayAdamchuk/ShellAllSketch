using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.IO;
using System.Windows.Media.Imaging;

using Autodesk; 
using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;

namespace ShellAllSketch
{
    /// <summary>
    /// Implements the Revit add-in interface IExternalApplication
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class ShellAllSketch : IExternalApplication
    {
        //public static AreaDirect areaDirect = AreaDirect.Main;
        //public static AreaLayer areaLayer = AreaLayer.Up;

        static string AddInPath = typeof(ShellAllSketch).Assembly.Location;
        // Button icons directory
        static string ButtonIconsFolder = Path.GetDirectoryName(AddInPath);
        static string AddInPathSketchFull = ButtonIconsFolder + "\\SketchFull_2023.dll";
        static string AddInPathSketchScheduler = ButtonIconsFolder + "\\SketchReinforcement_2023.dll";
        static string AddInPathSketchTag = ButtonIconsFolder + "\\SketchTag_2023.dll";

        #region IExternalApplication Members
        /// <summary>
        /// Implements the OnShutdown event
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        /// <summary>
        /// Implements the OnStartup event
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication application)
        {            
            Autodesk.Revit.ApplicationServices.LanguageType lt = application.ControlledApplication.Language;
            
            if (lt.ToString() == "Russian") Resourses.Strings.Texts.Culture= new System.Globalization.CultureInfo("ru-RU");

            RibbonPanel ribbonPanel = application.CreateRibbonPanel(Resourses.Strings.Texts.NamePanel);
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

#if Full
            PushButtonData styleSettingButton = new PushButtonData("DatumStyle", Resourses.Strings.Texts.NameImageSketchFull, AddInPathSketchFull, "SketchFull.SketchCommand");
            styleSettingButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ButtonIconsFolder + "\\Resources\\Images\\", "RebarDrawing.png"), UriKind.Absolute)); ;

            PushButtonData styleSettingButton2 = new PushButtonData("DatumStyle2", Resourses.Strings.Texts.NameImageSketchReinforcement, AddInPathSketchScheduler, "SketchReinforcement.SketchCommand");
            styleSettingButton2.LargeImage = new BitmapImage(new Uri(Path.Combine(ButtonIconsFolder + "\\Resources\\Images\\", "RebarSchedule.png"), UriKind.Absolute)); ;
                                   
            //string path = "c:\\Users\\Public\\Documents\\Autodesk\\Downloaded Content\\Ar-Cadia\\SketchReinforcement";
            ContextualHelp ch = new ContextualHelp(ContextualHelpType.ChmFile, path + Resourses.Strings.Texts.pathToHelpSchedule);
            styleSettingButton.SetContextualHelp(ch);             
            ribbonPanel.AddItem(styleSettingButton);

            ContextualHelp ch2 = new ContextualHelp(ContextualHelpType.ChmFile, path + Resourses.Strings.Texts.pathToHelpSchedule);
            styleSettingButton2.SetContextualHelp(ch2);             
            ribbonPanel.AddItem(styleSettingButton2);
#endif

#if Tag

            PushButtonData styleSettingButton3 = new PushButtonData("DatumStyle3", Resourses.Strings.Texts.NameImageSketchTag, AddInPathSketchTag, "SketchTag.SketchCommand");
            styleSettingButton3.LargeImage = new BitmapImage(new Uri(Path.Combine(ButtonIconsFolder + "\\Resources\\Images\\", "RebarTag.png"), UriKind.Absolute)); ;
            ContextualHelp ch3 = new ContextualHelp(ContextualHelpType.ChmFile, path + Resourses.Strings.Texts.pathToHelpTag);
            styleSettingButton3.SetContextualHelp(ch3);
            ribbonPanel.AddItem(styleSettingButton3);

#endif 

            return Result.Succeeded;
        }

        #endregion
    }
}
