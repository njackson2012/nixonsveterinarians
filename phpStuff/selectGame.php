<?php
	$servername = "den1.mysql4.gear.host";
	$username = "nixondb";
	$password = "Cy5G?_5x09e9";
	$dbname = "nixondb";
	
    $dbconn = mysql_connect($servername, $username, $password, $dbname) or die('Could not connect: ' . mysql_error()); 
	
	$gameid = mysql_real_escape_string($_GET['GAMEID'], $dbconn); 
	
	$query = "select * from games where GAMEID = '$gameid'";
	$result = mysql_query($query) or die('Query failed: ' . mysql_error());
	
	$num_results = mysql_num_rows($result);  
 
    for($i = 0; $i < $num_results; $i++)
    {
         $row = mysql_fetch_array($result);
         echo $row['GAMEID'] . "\t" . $row['GAMESTATUS'] . "\t" . $row['REQUESTSTATUS'] . "\t" . row['PLAYERTURN'] . "\n";
    }
?>