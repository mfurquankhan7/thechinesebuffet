<?PHP
    $html = '';
    $query = mysql_query("SELECT * FROM tbl_locations ORDER BY cLocation asc") or die(mysql_error());
    while($row = mysql_fetch_assoc($query)){
        if($location == $row['id']){
            $html .= '<option value="'.$row['id'].'" selected="selected">'.$row['cLocation'].'</option>';
        }else{
            $html .= '<option value="'.$row['id'].'">'.$row['cLocation'].'</option>';
        }
    }
    echo $html;
?>
