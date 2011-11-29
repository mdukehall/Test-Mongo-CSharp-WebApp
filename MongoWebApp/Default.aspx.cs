using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Driver;

namespace MongoWebApp
{
    public partial class Default:System.Web.UI.Page
    {
        protected void Page_Load(object sender,EventArgs e)
        {

        }

        protected void CreateMongoDB(String dbName)
        {
            //mongod.exe must be running for this to work.
            var mongo = new Mongo();
            mongo.Connect();

            //if the database is not found in c:\data\db it will be created.
            var db = mongo.GetDatabase(dbName);

            //declare a new "table"
            var categories = db.GetCollection("categories");

            //create a new key value set
            var document = new Document();
            document["Name"] = "Product";
            document["Name"] = "Price";


            //create the "tabt"
            categories.Insert(document);
            mongo.Disconnect();
        }

        protected String GetMongoDBResults(String dbName)
        {
            //mongod.exe must be running for this to work.
            var mongo = new Mongo();
            mongo.Connect();

            //if the database is not found in c:\data\db it will be created.
            var db = mongo.GetDatabase(dbName);
            
            //declare a new "table"
            var categories = db.GetCollection("categories");

            //get the categories table
            var category = categories.FindOne(new Document() { { "Name","Price" } });
            //alternatively you can get all documents in the database
            var documents = categories.FindAll().Documents;
            String res = category["Name"].ToString();
            
            //diconnect
            mongo.Disconnect();

            //return results in a EF friendly shapre just because that's what I'm working with mostly nowadays
            return res;

        }

        protected void Button1_Click(object sender,EventArgs e)
        {
            String name = TextBox1.Text;
            CreateMongoDB(name);
        }

        protected void Button2_Click(object sender,EventArgs e)
        {
            RadGrid1.DataSource = GetMongoDBResults(TextBox1.Text);
            RadGrid1.DataBind();
        }
    }
}