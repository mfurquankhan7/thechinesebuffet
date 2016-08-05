<?PHP
    include('../connection.php');
    $id = $_GET['id'];
    $query = mysql_query("SELECT * FROM tbl_locations WHERE id='$id'")or die(mysql_error());
    $row = mysql_fetch_assoc($query);

    $query_time =  mysql_query("SELECT * FROM tbl_timings WHERE nRefId='$id'")or die(mysql_error());
    $days = '';
    $time = '';
    while($row_time = mysql_fetch_assoc($query_time)){
        $days .= '<p>'.$row_time['cDays'].'</p>';
        $time .= '<p>'.$row_time['cTiming'].'</p>';
    }

    echo $row['cAddress'].'==='.$row['cContact'].'==='.$row['cMap'].'==='.$days.'==='.$time;

?>