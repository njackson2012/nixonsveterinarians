<?php
	$servername = "den1.mysql4.gear.host";
	$username = "nixondb";
	$password = "Cy5G?_5x09e9";
	$dbname = "nixondb";
	
    $dbconn = mysql_connect($servername, $username, $password, $dbname) or die('Could not connect: ' . mysql_error()); 
 
	// Strings must be escaped to prevent SQL injection attack. 
	$gameid = mysql_real_escape_string($_GET['GAMEID'], $dbconn); 
	$gamestatus = mysql_real_escape_string($_GET['GAMESTATUS'], $dbconn);
	$requeststatus = mysql_real_escape_string($_GET['REQUESTSTATUS'], $dbconn);
	$playerturn = mysql_real_escape_string($_GET['PLAYERTURN'], $dbconn); 
	
	if (is_null($gameid)){
		$query = "insert into games (GAMESTATUS, REQUESTSTATUS, PLAYERTURN) values ('$gamestatus', '$requeststatus', '$playerturn');";
		$result = mysql_query($query) or die('Query failed: ' . mysql_error());
	} else {
		$query = "update games set GAMESTATUS = '$gamestatus', REQUESTSTATUS = '$requeststatus', PLAYERTURN = '$playerturn' where GAMEID = '$gameid'";
		$result = mysql_query($query) or die('Query failed: ' . mysql_error());
	}
?>