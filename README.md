Project has been developed in Microsoft .NET MVC Framework 4.7

<b>Prerequisite</b></br>
	SqlServer or SQLExpress installed on local machine

<b>Installation Steps</b></br>
	1) Open the solution file ImageProcessingTool.sln on Visual Studio<br>
	2) Perform a Clean, Build and Run
	
	The application create a database "MySqlDB2" as per the connection string mentioned in the web.conf file 
		<add name="sqlServerConnection" 
		 	providerName="System.Data.SqlClient" 
			connectionString="Data Source=localhost;Initial Catalog=MySqlDB2;Integrated Security=True;" />  
<b>Work flow</b></br>
	1) The application will lead to the landing page http://localhost:56232/Image/Add</br>
	2) On the Add page, the image files can be uploaded  </br>
	3) Once the selected files are uploaded, the page get refreshed and append the uploaded  files below the title "Uploaded Images"</br>
	4) Upon clicking on the any uploaded file, it will lead to another browser tab, where the original file and greyed file are listed side by side 


 
