<div class="logo">

    <a href="home.php" target="_self">
        <img src="images/layout/the-chinese-buffet-logo.png" />
    </a>

</div>
<!--/.logo-->

<nav id="main-nav">
    <ul>
        <?PHP
            $query_menu = mysql_query("SELECT * FROM tbl_navigation WHERE isMain='1' ORDER BY nOrder asc") or die(mysql_error());
            $menuHTML = '';
            while($row_menu = mysql_fetch_assoc($query_menu)){
                $tempURL = strtolower($row_menu['cLinks']);
                $url = preg_replace('/\s+/', '-', $tempURL);

                if($mainLink == $row_menu['id']){
                    $menuHTML .= '<li><a href="'.$url.'" target="_self" class="active">'.$row_menu['cLinks'].'</a>'."\n";
                }else{
                    $menuHTML .= '<li><a href="'.$url.'" target="_self">'.$row_menu['cLinks'].'</a>'."\n";        
                }
                if($row_menu['isSub'] == 1){
                    $tempId = $row_menu['id'];
                    $query_submenu = mysql_query("SELECT * FROM tbl_navigation WHERE refId='$tempId'") or die(mysql_error());
                    $menuHTML .= '<ul>'."\n";        
                    while($row_submenu = mysql_fetch_assoc($query_submenu)){
                        $tempURL = strtolower($row_submenu['cLinks']);
                        $url = preg_replace('/\s+/', '-', $tempURL);                
                        if($mainLink == $row_submenu['id']){
                            $menuHTML .= '<li class="active"><a href="'.$url.'" target="_self" class="active">'.$row_submenu['cLinks'].'</a>'."\n";
                        }else{
                            $menuHTML .= '<li><a href="'.$url.'" target="_self">'.$row_submenu['cLinks'].'</a>'."\n";       
                        }
                    }
                    $menuHTML .= '</ul>'."\n";   
                }
                $menuHTML .= '</li>'."\n";   
            }
            echo $menuHTML;
        ?>
    </ul>
</nav>
<!--/#main-nav-->
