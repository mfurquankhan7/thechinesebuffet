<?PHP
    $locationhtml = '';
    $query_location = mysql_query("SELECT * FROM tbl_locations ORDER BY cLocation asc") or die(mysql_error());
    while($row_location = mysql_fetch_assoc($query_location)){
        $locationhtml .= '<option value="'.$row_location['id'].'-'.$row_location['nIsMenu'].'">'.$row_location['cLocation'].'</option>';
    }
    echo $locationhtml;
?>
