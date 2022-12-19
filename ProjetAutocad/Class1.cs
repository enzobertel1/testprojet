using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;


namespace ProjetAutocad
{
    internal class Initilization :IExtensionApplication
    {

        #region Commands
        [CommandMethod("MaCommande1")]
        public void cmd1()
        {
            var doc = AcAp.DocumentManager.MdiActiveDocument;

            var db = doc.Database;
            var ed = doc.Editor;

            ed.WriteMessage("\nI have created my first command");

            using (var tr = db.TransactionManager.StartTransaction())
            {
                BlockTable blkbl = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord msBlRec = tr.GetObject(blkbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                Point3d pt1 = new Point3d(0, 0,0);
                Point3d pt2 = new Point3d(10, 10,0);

                Line ligneObj = new Line(pt1, pt2); // Créé une ligne entre les deux points
                msBlRec.AppendEntity(ligneObj); 
                tr.AddNewlyCreatedDBObject(ligneObj, true); // trace la ligne avec les deux points

                tr.Commit();
            }

        }
        #endregion


        void IExtensionApplication.Initialize()
        {

        }

        void IExtensionApplication.Terminate()
        {

        }
        //endregion
    }
}
