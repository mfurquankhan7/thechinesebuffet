<?PHP
    include('../connection.php');
    $locId = $_GET['id'];    
    $itemHTML = '';
    $query_Item = mysql_query("SELECT * FROM tbl_items WHERE nLocationId='$locId'") or die(mysql_error());
    if(mysql_num_rows($query_Item) != 0){
        while($row_Items = mysql_fetch_assoc($query_Item)){        
             $itemHTML .= '<h5>'.$row_Items['cItemName'].'</h5>';
        }
    }else{
        $itemHTML .= '<h5>No item found...!</h5>';
    }
    echo $itemHTML;
?>
