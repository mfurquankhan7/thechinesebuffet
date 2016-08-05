<?PHP

	//$username	= "root";

//	$password	= "admin123";

//	$hostname	= "localhost";

//	$dbname	  = "db_india-bistro";

	

	$username	= "nativeas_tcb";
	$password	= "Shayaan12345";
	$hostname	= "localhost";
	$dbname	  = "nativeas_tcb";





	

	$connect = mysql_connect($hostname, $username, $password) or die ("Could not connect to database");

	mysql_select_db($dbname) or die ("Could not find database");

?>