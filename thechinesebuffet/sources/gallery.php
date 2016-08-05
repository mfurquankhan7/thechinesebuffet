<?PHP
    $html = '';
    $query = mysql_query("SELECT * FROM tbl_gallery WHERE nLocationId='$location' AND nTypeId='$type' ORDER BY RAND()")or die(mysql_error());
    //$row = mysql_fetch_assoc($query);

    while($row = mysql_fetch_assoc($query)){
        $html .= '<div class="item">'."\n";
        $html .= '<a class="fancybox" href="images/gallery/'.$row['cImageBigURL'].'" data-fancybox-group="gallery1">'."\n";
        $html .= '<img src="images/gallery/'.$row['cImageURL'].'" />'."\n";
        $html .= '</a>'."\n";
        $html .= '</div><!--/.item-->'."\n";
    }
    echo $html;
?>
