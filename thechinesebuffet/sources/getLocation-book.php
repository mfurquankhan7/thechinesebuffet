<?PHP
    $html = '';
    $query = mysql_query("SELECT * FROM tbl_locations ORDER BY cLocation asc") or die(mysql_error());
    while($row = mysql_fetch_assoc($query)){
        $html .= '<option value="'.$row['cLocation'].'">'.$row['cLocation'].'</option>';
    }
    echo $html;
?>
