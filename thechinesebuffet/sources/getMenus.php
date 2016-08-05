<?PHP
    include('../connection.php');
    $id = $_GET['id'];
    $menuhtml = '<option val="0-0">Select Menu</option>';
    $query_menus = mysql_query("SELECT * FROM tbl_menus WHERE nLocationId='$id'") or die(mysql_error());
    while($row_menus = mysql_fetch_assoc($query_menus)){
        $menuhtml .= '<option value="'.$row_menus['id'].'-'.$id.'">'.$row_menus['cMenuName'].'</option>';
    }
    echo $menuhtml;
?>
