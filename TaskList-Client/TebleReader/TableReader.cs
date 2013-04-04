using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace TableReader
{
    
    public class UserManager
    {
        public static string EncodePassword(string Password)
        {
            Byte[] encoded;
            Byte[] raw;
            MD5 md5;
            md5 = new MD5CryptoServiceProvider();
            raw = Encoding.UTF8.GetBytes(Password);
            encoded = md5.ComputeHash(raw);
            
            return BitConverter.ToString(encoded);
        }
    }

    public  class MongoManager {
        static MongoClient client;
        static MongoServer server;
        public static MongoDatabase UsersDB;
        public static MongoCollection<BsonDocument> UsersCollection;
        public MongoManager()
        {
            client = new MongoClient("mongodb://192.168.1.104"); // connect to localhost
            server = client.GetServer();
            UsersDB = server.GetDatabase("users");
            UsersCollection = UsersDB.GetCollection("users");
        }
    }

    public class TaskUser
    {
        //private String name;
        private String pass;
        public String Name
        {
            get;
            set;
        }

        public String Pass
        {
            get 
            {
                return pass; 
            }
            set 
            {
                pass = UserManager.EncodePassword(value).Replace("-","").ToLower(); 
            }
        }

        public List<TaskTable> Tables
        {
            get;
            set;
        }
        public TaskUser()
        {
            this.Tables = new List<TaskTable>();
        }
        public TaskUser(String uname,String upass)
        {
            this.Name = uname;
            this.Pass = upass;
            this.Tables = new List<TaskTable>();
        }

     
        public void Login()
        {
            try
            {
                var res = MongoManager.UsersCollection.FindOne(Query.And(Query.EQ("name", this.Name), Query.EQ("pass", this.Pass)));
                
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid Username or Password");
                return;
            }
            throw new Exception("Loged In");
        }

        public void Signup()
        {
            /* BsonDocument doc = new BsonDocument{
                {"name",this.Name},
                {"pass",this.Pass}
            };
            MongoManager.UsersCollection.Save(doc); */
            MongoManager.UsersCollection.Insert(this);
        }

        public override string ToString()
        {
            return this.Name + " - " + this.Pass;
        }



    }

    public class TaskTable
    {
        public String name
        {
            get;
            set;
        }

        void add() { 
        
        }

        public List<Task> Tasks
        {
            get;
            set;
        }
    }

    public class Task
    {
        public String name
        {
            get;
            set;
        }

        public int status
        {
            get;
            set;
        }

        public int location
        {
            get;
            set;
        }
    }

}
