using AuthorizationServer.Models;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AuthorizationServer
{
    public class Config
    {
        
        private readonly ApplicationDbContext _context;

        public Config(ApplicationDbContext context)
        {
        _context = context;
        }
        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
      {
        new ApiResource("scope.readaccess", "Example API"),
        new ApiResource("scope.fullaccess", "Example API"),
      };
        }

        // client want to access resources (aka scopes)

        public static  IEnumerable<Client> GetClients()
        {
            string scope;
            List<Client> clients = new List<Client>();
            SqlConnection chessdb = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=ChessDB;Trusted_Connection=True;");


            try
            {
                chessdb.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("Select * from dbo.Users", chessdb);
                myReader = myCommand.ExecuteReader();


                while (myReader.Read())
                {

                    string username = myReader["UserName"].ToString();
                    string password = myReader["Password"].ToString();
                    string role = myReader["Role"].ToString();

                    if (role == "admin")
                    {
                        scope = "scope.fullaccess";
                    }
                    else scope = "scope.readaccess";
                    Client client = new Client
                    {
                        ClientId = username,
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets =
            {
              new Secret(username.Sha256())
            },
                        AllowedScopes = { scope }
                    };
                    clients.Add(client);
                };

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return clients;

        }


    }

}