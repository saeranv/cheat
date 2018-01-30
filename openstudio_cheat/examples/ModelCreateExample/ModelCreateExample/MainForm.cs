using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RunManagerExample
{
    public partial class MainForm : Form
    {
        private OpenStudio.RunManager m_runmanager;

        public MainForm()
        {
            try
            {
                m_runmanager = new OpenStudio.RunManager(new OpenStudio.Path("runmanager.db"));
            }
            catch (Exception e)
            {
              MessageBox.Show("It appears that there was an error accessing the C# SWIG Bindings for OpenStudio. Note that the libraries installed in <installdir>/CSharp/openstudio need to be accessible to this application at runtime, either through the path or in the same directory as the exe. Nothing else will work properly have this point.\n\nError Text: " + e.InnerException.InnerException.Message, "Error loading libraries");
            }

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnCreateModel_Click(object sender, EventArgs e)
        {
          SaveFileDialog sfd = new SaveFileDialog();

          sfd.FileName = "in";
          sfd.DefaultExt = "osm";
          sfd.Filter = "OpenStudio Model (*.osm)|*.osm";
          sfd.CheckPathExists = true;
          sfd.OverwritePrompt = true;

          if (sfd.ShowDialog() == DialogResult.OK)
          {
            string fname = sfd.FileName;

            OpenStudio.Model osm = new OpenStudio.osm();

            OpenStudio.Construction construction = new OpenStudio.Construction(osm);
            construction.setName("Construction");

            //ASSERT_TRUE(construction.name());
            //EXPECT_EQ("Construction", construction.name().get());

            OpenStudio.Space space = new OpenStudio.Space(osm);
            space.setName("Space");

            //ASSERT_TRUE(zone.name());
            //EXPECT_EQ("Zone", zone.name().get());

            OpenStudio.Point3dVector points = new OpenStudio.Point3dVector();

            points.Add(new OpenStudio.Point3d(0, 0, 1));
            points.Add(new OpenStudio.Point3d(0, 0, 0));
            points.Add(new OpenStudio.Point3d(1, 0, 0));
            points.Add(new OpenStudio.Point3d(1, 0, 1));

            OpenStudio.Surface roof = new OpenStudio.Surface(points, osm);
            roof.setName("Roof");
            roof.setSpace(space);
            roof.setSurfaceType("Roof");
            roof.setConstruction(construction);

            //ASSERT_TRUE(roof.name());
            //EXPECT_EQ("Roof", roof.name().get());
            //ASSERT_TRUE(roof.construction());
            //EXPECT_EQ(construction.handle(), roof.construction()->handle());

            OpenStudio.Surface wall = new OpenStudio.Surface(points, osm);
            wall.setName("Wall");
            wall.setSpace(space);

            wall.setSurfaceType("Wall");
            wall.setConstruction(construction);

            //ASSERT_TRUE(wall.name());
            //EXPECT_EQ("Wall", wall.name().get());
            //ASSERT_TRUE(wall.construction());
            //EXPECT_EQ(construction.handle(), wall.construction()->handle());

            OpenStudio.SubSurface window = new OpenStudio.SubSurface(points, osm);
            window.setName("Window");
            window.setSurface(wall);
            window.setSubSurfaceType("Window");
            window.setConstruction(construction);

            //ASSERT_TRUE(window.name());
            //EXPECT_EQ("Window", window.name().get());
            //ASSERT_TRUE(window.construction());
            //EXPECT_EQ(construction.handle(), window.construction()->handle());

            OpenStudio.Surface floor = new OpenStudio.Surface(points, osm);
            floor.setName("Floor");
            floor.setSpace(space);
            floor.setSurfaceType("Floor");
            floor.setConstruction(construction);

            if (osm.save(new OpenStudio.Path(fname), true))
            {
              MessageBox.Show("model saved to: " + fname);
            }
            else
            {
              MessageBox.Show("Error saving model to: " + fname);
            }

          }

        }

    }
}
