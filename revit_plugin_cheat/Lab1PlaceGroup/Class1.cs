using System; //
using System.Collections.Generic;
using System.Linq;

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;

/* .NET Framework contains 2 main components:
 * 1. Common Language Runtime (CLR)
 *  Execution agent in .NET. Manages code by providing core services like memory management, 
 *  exception handling, managing multiple threads. 
 *  When we build code into DLL, it is compiled to Common Intermediate Language (CIL) code using 
 *  language specific compiler. CIL is CPU-independant set of instructions that can be 
 *  executed by the CLR on windows.
 * 2. .NET Framework Class Library 
 *  Collection of ojbect s for Windows including Windows GUI.
*/

/* Visual Studio IDE
 * Contains: Solution Files, Project files, and Project Items
 * Solution Files: container for one or more projects
 * Project Files: container for project items (source files, icons) 
 *  this gets compiled into the executable file (exe or dll)
 *  Visual Studio will load and display solution in right hand tab
 */

/* 
 * 
 * 
 */ 


[TransactionAttribute(TransactionMode.Manual)] //?
[RegenerationAttribute(RegenerationOption.Manual)]//?

public class Lab1PlaceGroup : IExternalCommand
{
    public Result Execute(
        ExternalCommandData commandData,
        ref string message,
        ElementSet elements)
    {
        //Get application and document objects
        UIApplication uiApp = commandData.Application;
        Document doc = uiApp.ActiveUIDocument.Document;

        //Define a Reference object to accept the pick result
        Reference pickedRef = null;

        //Pick a group
        Selection sel = uiApp.ActiveUIDocument.Selection;
        pickedRef = sel.PickObject(ObjectType.Element, "Please select a group!");
        Element elem = doc.GetElement(pickedRef);
        Group group = elem as Group;

        //Pick a point
        XYZ point = sel.PickPoint("Please pick a point to place group");

        //Place the group
        Transaction trans = new Transaction(doc);
        trans.Start("Lab");
        doc.Create.PlaceGroup(point, group.GroupType);
        trans.Commit();

        return Result.Succeeded;
    }
}

