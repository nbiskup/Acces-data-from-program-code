The first application is a SQL Manager Windows Forms application, which enable the execution of DDL and DML queries, as well as the display of query results and messages in a nicely formatted way. The application is based on the SSMS Query window and enable login to the SQL Server. Repository Factory pattern and the Lazy paradigm is used.

The second application is a WPF application that allows CRUD operations on related entities and it's implemented using the MVVM pattern, Observable collections, and resources and styles in the WPF application. It validate data, navigate within the Window Frame element, display images in the ToolTip element, and work with binary data.

The third application is an MVC application that allows CRUD operations on related entities and should be implemented using the Entity Framework ORM and an unlimited number of images. All forms are validated, and the application is published on Azure cloud. The database is set up on Azure cloud and the Entity Framework ORM is used.

In the fourth application is implemented a client application that allows uploading, downloading, displaying, and deleting JPEG, TIFF, PNG, SVG, GIF files on Azure cloud. Different formats are stored in directories with the same names, and Lazy Repository is used to access the BlobContainer entity and async/await asynchronous programming.

In the fifth application is implemented an MVC application that allows CRUD operations on the Person entity, using AzureCosmosDB NoSql database on Azure cloud. All forms are validated, and data handling is asynchronous.
