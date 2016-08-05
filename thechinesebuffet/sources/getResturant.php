<?PHP
    include('../connection.php');
    $id = $_GET['id'];
    $query = mysql_query("SELECT * FROM tbl_locations WHERE id='$id'")or die(mysql_error());
    $row = mysql_fetch_assoc($query);
    echo '<p>'.$row['cContent'].'</p>';
    echo '<p class="phone"><i class="fa fa-phone fa-lg" aria-hidden="true"></i> '.$row['cContact'].'</p>';
?>
