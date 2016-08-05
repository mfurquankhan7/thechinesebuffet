<?PHP
    include('../connection.php');
    $menuId = $_GET['id'];
    $locId = $_GET['locId'];    
    $itemHTML = '';
    $query_Item = mysql_query("SELECT * FROM tbl_items WHERE nMenuId='$menuId' AND nLocationId='$locId'") or die(mysql_error());
    while($row_Items = mysql_fetch_assoc($query_Item)){        
        $itemHTML .= '<h5>'.$row_Items['cItemName'].'</h5>';
    }
    echo $itemHTML;
?>
