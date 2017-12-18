using System; //
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;


using Autodesk.Revit.DB; // RevitAPI.dll = Revit application, documents, elements, parameters
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI; // Manipulation and customization of Revit UI, command, selections and dialogs
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;

/* Visual Studio IDE
 * Contains: Solution Files, Project files, and Project Items
 * Solution Files: container for one or more projects
 * Project Files: container for project items (source files, icons) 
 *  this gets compiled into the executable file (exe or dll)
 *  Visual Studio will load and display solution in right hand tab
 */

/* API refrences. Set Copy Local to False (so this doesn't get copied with assembly DLL)
 * They are already linked to DLLS in the Revit folder, so it would confuse the CLR if have to 
 * look for two location.
 */

/* .NET Framework contains 2 main components:
 * 1. Common Language Runtime (CLR): CPU independent that can be run (in CIL using CLR 
 *  Execution agent in .NET. Manages code by providing core services like memory management, 
 *  exception handling, managing multiple threads. 
 *  When we build code into DLL, it is compiled to Common Intermediate Language (CIL) code using 
 *  language specific compiler. CIL is CPU-independant set of instructions that can be 
 *  executed by the CLR on windows.
 * 2. .NET Framework Class Library 
 *  Collection of ojbect s for Windows including Windows GUI.
*/

/* More on CLR
 * when we build code into DLL this is what happens:
 * - it is compiled to Common Intermediate Language (CIL) (aka bytecode)
 * - have compilers to translate source code to CIL (in dll) by langauge i.e. ironpython, c#
 * - This is then packaged into a .NET assembly
 * - .NET Assembly is library of CIL code stored with metadata
 * - .NET Assembly: can either be process assemblies (EXEs) or library assemblies (DLLs)
 */


/* Transaction: Set an attribute to control the transaction behaviour of the commnad 
 * - Transaction are objects that capture changes to the Revit model
 * - Changes to the Revit model can only be performed when there is an active transaction to do so
 * - Transactions can be committed (recorded in model) or rolled-back (changes are done)
 * - Set this to manual: revit does not create transaction automatically. We must create, name, commit transactions
 */

[TransactionAttribute(TransactionMode.Manual)] 
[RegenerationAttribute(RegenerationOption.Manual)]//?

/* When we do this Lab1PlaceGroup : IExternalCommand
 * - class will implement interface called Autodesk.Revit.UI.IExternalCommand
 * - in Revit this is consiered an external command
 * - Revit sees uses this class to execute command when it gets triggered from product's user interface
 * - Interfaces are like classes
 * - Revit will find this class, and then run the .Execute() method on it
 */

public class Lab1PlaceGroup : IExternalCommand
{
    public Result Execute( // Revit will find and run this method if you use the IExternalCommand interface
        // Param 1: Autodesk.Revit.UI.ExternalCommandData, provides API access to the revit application
        // - access to the document that is active in the user interface and corresponding DATABASE!
        ExternalCommandData commandData, 
        // Param 2: string parameter, ref means it can be modified in method implementation
        ref string message,
        // Param 3: Autodesk.Revit.DB.ElementSet, choose elements to be highlighted on screen
        ElementSet elements)
    {
        //Get application and document objects
        UIApplication uiApp = commandData.Application; // access application UI, variable type is UIApplication obj
        Document doc = uiApp.ActiveUIDocument.Document; // This is the revit database stored in doc


        //Define a Reference object to accept the pick result
        // Reference == class containing elements from a Revit model associated with valid geometry.
        Reference pickedRef = null; // 

        //Pick a group
        Selection sel = uiApp.ActiveUIDocument.Selection; //retrieve the selected elements
        pickedRef = sel.PickObject(ObjectType.Element, "Please select a group!"); // User prompt
        Element elem = doc.GetElement(pickedRef); //store it as a element
        Group group = elem as Group; // cast the element as a group

        //Pick a point
        XYZ point = sel.PickPoint("Please pick a point to place group"); // pick a point using selection class again

        //Place the group
        // We need to manage our own transactions
        // -in this case: define new group that we are copying into model
        Transaction trans = new Transaction(doc); // new trans into the doc
        trans.Start("Lab"); // name the transaction
        doc.Create.PlaceGroup(point, group.GroupType); // place the group in the model
        trans.Commit(); // commit changes (soave)

        // Autodesk.Revit.UI.Result will tell Revit whether the command has succeeded/failed
        return Result.Succeeded;
    }
}

