using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace PhotoCloud
{
    
    public class Password
    {
        string hashVal, saltVal;

        public string Hash
        {
            get { return hashVal; }
            set { hashVal = value; }
        }

        public string Salt
        {
            get { return saltVal; }
            set { saltVal = value; }
        }

        public void SaltedHash(string password)
        {
            var saltBytes = new byte[32]; 

            using (var provider = new RNGCryptoServiceProvider())
                provider.GetNonZeroBytes(saltBytes);    
            saltVal = Convert.ToBase64String(saltBytes); 
            hashVal = ComputeHash(saltVal, password);
        }

        static string ComputeHash(string salt, string password)
        {
            var saltBytes = Convert.FromBase64String(salt);
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 1000))
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(16));
        }

        public bool Verify(string salt, string hash, string password)
        {
            return hash == ComputeHash(salt, password);
        }
    }

    public class Photos
    {
        private const string connection = @"Data Source=TANYA-PC\SQLEXPRESS; Initial Catalog=PhotoStorage;Integrated Security=True;";

        private DataTable dtPhotos;
        private SqlDataAdapter daPhotos;

        private int albumId;
        private int locationId;
        private string fileName;
        private byte data;
        private double width;
        private double height;
        private DateTime date;

        public Photos(){
            loadPhotos();
        }

        public Photos(int albumId)
        {
            loadPhotos(albumId);
        }

        public int AlbumID
        {
            get { return albumId; }
            set { albumId = value; }
        }

        public int LocationID
        {
            get { return locationId; }
            set { locationId = value; }
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public byte Data
        {
            get { return data; }
            set { data = value; }
        }

        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public DataTable DataTablePhotos
        {
            get { return dtPhotos; }
            set { dtPhotos = value; }
        }

        public void loadPhotos(int albumId = -1)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            dtPhotos = new DataTable();

            string cmd =  "Select PhotoID, AlbumID, LocationID, FileName, Data, Width, Height, Date from Photos";
            if(albumId != -1)
            {
                cmd += " where AlbumID = " + albumId;
            }

            daPhotos = new SqlDataAdapter(cmd, conn);
            daPhotos.Fill(dtPhotos);
        }

        public void updatePhotos()
        {
            SqlCommandBuilder cmb = new SqlCommandBuilder(daPhotos);
            daPhotos.InsertCommand = cmb.GetInsertCommand();
            daPhotos.DeleteCommand = cmb.GetDeleteCommand();
            daPhotos.UpdateCommand = cmb.GetUpdateCommand();
              
            daPhotos.Update(dtPhotos);
        }
    }

    public class Albums
    {
        private const string connection = @"Data Source=TANYA-PC\SQLEXPRESS; Initial Catalog=PhotoStorage;Integrated Security=True;";
        private DataTable dtAlbums;
        private SqlDataAdapter daAlbums;
        private DataTable dtUsersAlbums;
        private SqlDataAdapter daUsersAlbums;

        private string name;
        private string description;
        private DateTime date;

        public Albums()
        {
            loadAlbums();
            loadUsersAlbums();
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public DataTable DataTableAlbums
        {
            get { return dtAlbums; }
            set { dtAlbums = value; }
        }

        public DataTable DataTableUsersAlbums
        {
            get { return dtUsersAlbums; }
            set { dtUsersAlbums = value; }
        }

        public void loadAlbums(int albumId = -1)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            dtAlbums = new DataTable();
            string cmd = "Select AlbumID, Name, Description, DateModified from Albums";
            daAlbums = new SqlDataAdapter(cmd, conn);
            daAlbums.Fill(dtAlbums);
            daAlbums.AcceptChangesDuringUpdate = false;
            
        }

        public void loadUsersAlbums()
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            dtUsersAlbums = new DataTable();
            string cmd = "Select AlbumID, UserID from UsersAlbums";
            daUsersAlbums = new SqlDataAdapter(cmd, conn);
            daUsersAlbums.Fill(dtUsersAlbums);

        }

        private SqlCommand getInserCommand(SqlCommandBuilder cmb)
        {
            SqlCommand cmd = (SqlCommand) cmb.GetInsertCommand().Clone();                                 
            cmd.CommandText += " SET @ID = SCOPE_IDENTITY()"; 

            // the SET command writes to an output parameter "@ID"
            SqlParameter parm = new SqlParameter();
            parm.Direction = ParameterDirection.Output;                   
            parm.Size = 4;
            parm.SqlDbType = SqlDbType.Int;
            parm.ParameterName = "@ID";
            parm.DbType = DbType.Int32;                                      

            cmd.Parameters.Add(parm);

            return cmd;
        }

        public void updateAlbums(List<UsersAlbums> listUA = null)
        {
            SqlCommandBuilder cmb = new SqlCommandBuilder(daAlbums);
            daAlbums.InsertCommand = getInserCommand(cmb);
            daAlbums.DeleteCommand = cmb.GetDeleteCommand();
            daAlbums.UpdateCommand = cmb.GetUpdateCommand();
            
            daAlbums.RowUpdated += new SqlRowUpdatedEventHandler((sender, e) => daAlbums_RowUpdated(sender, e, listUA));
            daAlbums.Update(dtAlbums);
            daAlbums.RowUpdated -= new SqlRowUpdatedEventHandler((sender, e) => daAlbums_RowUpdated(sender, e, listUA));

            if(listUA != null)
            {
                foreach (UsersAlbums ua in listUA)
                {
                    DataRow dr = dtUsersAlbums.NewRow();
                    dr["UserID"] = ua.UserId;
                    dr["AlbumID"] = ua.AlbumId;
                    dtUsersAlbums.Rows.Add(dr);
                } 
            }
            updateUsersAlbums();
        }

        public void updateUsersAlbums()
        {
            SqlCommandBuilder cmb = new SqlCommandBuilder(daUsersAlbums);
            daUsersAlbums.InsertCommand = cmb.GetInsertCommand();
            daUsersAlbums.DeleteCommand = cmb.GetDeleteCommand();
            daUsersAlbums.UpdateCommand = cmb.GetUpdateCommand();

            daUsersAlbums.Update(dtUsersAlbums);
        }

        private static void daAlbums_RowUpdated(object sender, SqlRowUpdatedEventArgs e, List<UsersAlbums> listUA)
        {
            if (e.StatementType == StatementType.Insert)
            {
                int id = Convert.ToInt32(e.Command.Parameters["@ID"].Value);

                foreach(UsersAlbums ua in listUA)
                {
                    ua.AlbumId = id;
                }

                e.Row.AcceptChanges();
            }
        }
    }

    public class Comments
    {

    }

    public class Categories
    {
        private const string connection = @"Data Source=TANYA-PC\SQLEXPRESS; Initial Catalog=PhotoStorage;Integrated Security=True;";

        private DataTable dtCategories;
        private SqlDataAdapter daCategories;

        private string name;
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public DataTable DataTableCategories
        {
            get { return dtCategories; }
            set { dtCategories = value; }
        }

        public void loadCategories(int albumId = -1)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            dtCategories = new DataTable();
            string cmd = "Select CategoryID, Name from Categories";
            daCategories = new SqlDataAdapter(cmd, conn);
            daCategories.Fill(dtCategories);
        }

        public void updateUsers()
        {
            SqlCommandBuilder cmb = new SqlCommandBuilder(daCategories);
            daCategories.InsertCommand = cmb.GetInsertCommand();
            daCategories.DeleteCommand = cmb.GetDeleteCommand();
            daCategories.UpdateCommand = cmb.GetUpdateCommand();
             
            daCategories.Update(dtCategories);
        }
    }

    public class Users
    {
        private const string connection = @"Data Source=TANYA-PC\SQLEXPRESS; Initial Catalog=PhotoStorage;Integrated Security=True;";
        
        private string firstName;
        private string lastName;
        private string email;
        private string password;
        private string salt;

        private DataTable dtUsers;
        private SqlDataAdapter daUsers;
        private DataTable dtAlbums;
        private SqlDataAdapter daAlbums;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Salt
        {
            get { return salt; }
            set { salt = value; }
        }

        public DataTable DataTableUsers
        {
            get { return dtUsers; }
            set { dtUsers = value; }
        }

        public DataTable DataTableAlbums
        {
            get { return dtAlbums; }
            set { dtAlbums = value; }
        }

        public void loadUsers()
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            dtUsers = new DataTable();
            string cmd ="Select UserID, FirstName, LastName, Email, Password, Salt from Users";
            daUsers = new SqlDataAdapter(cmd,conn);
            daUsers.Fill(dtUsers);
        }

        public void loadUsersAlbums(int userId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            dtAlbums = new DataTable();

            string cmd = "Select AlbumID, Name, Description, DateModified from Albums where AlbumID in (select AlbumID from UsersAlbums where UserID = " + userId + ")";
            daAlbums = new SqlDataAdapter(cmd, conn);
            daAlbums.Fill(dtAlbums);
        }

        public void updateUsers()
        {
            SqlCommandBuilder cmb = new SqlCommandBuilder(daUsers);
            daUsers.InsertCommand = cmb.GetInsertCommand();
            daUsers.DeleteCommand = cmb.GetDeleteCommand();
            daUsers.UpdateCommand = cmb.GetUpdateCommand();

            daUsers.Update(dtUsers);
        }
    }

    public class UsersAlbums{
        private int albumId;
        private int userId;

        public int UserId
        {
            get { return userId; }
            set{ userId = value;}
        }

        public int AlbumId
        {
            get { return albumId; }
            set { albumId = value; }
        }
    }

    public class Locations
    {

    }
}