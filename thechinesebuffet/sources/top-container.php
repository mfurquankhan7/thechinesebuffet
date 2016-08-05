<div class="top-container-inner top-left">
    <ul>
        <?PHP
            if($topLink == 1){
                echo '<li><a href="login" target="_self" class="active">Login</a></li>';
                echo '<li><a href="signup" target="_self">Sign Up</a></li>';
                echo '<li><a href="careers" target="_self">Careers</a></li>';
            }else if($topLink == 2){
                echo '<li><a href="login" target="_self">Login</a></li>';
                echo '<li><a href="signup" target="_self" class="active">Sign Up</a></li>';
                echo '<li><a href="careers" target="_self">Careers</a></li>';
            }else if($topLink == 3){
                echo '<li><a href="login" target="_self">Login</a></li>';
                echo '<li><a href="signup" target="_self">Sign Up</a></li>';
                echo '<li><a href="careers" target="_self" class="active">Careers</a></li>';
            }else{
                echo '<li><a href="login" target="_self">Login</a></li>';
                echo '<li><a href="signup" target="_self">Sign Up</a></li>';
                echo '<li><a href="careers" target="_self">Careers</a></li>';
            }
        ?>
    </ul>

    <div class="social-follow">
        <div class="fb-like" data-href="https://www.facebook.com/TheChineseBuffet/" data-layout="button_count" data-action="like" data-size="small" data-show-faces="true" data-share="false"></div>
        <div id="fb-root"></div>
        <script>
            (function(d, s, id) {
                var js, fjs = d.getElementsByTagName(s)[0];
                if (d.getElementById(id)) return;
                js = d.createElement(s);
                js.id = id;
                js.src = "//connect.facebook.net/en_GB/sdk.js#xfbml=1&version=v2.7";
                fjs.parentNode.insertBefore(js, fjs);
            }(document, 'script', 'facebook-jssdk'));

        </script>
    </div>
    <div class="social-follow">
        <a href="https://twitter.com/TChineseBuffet" class="twitter-follow-button" data-show-screen-name="false" data-show-count="true">Follow @TChineseBuffet</a>
        <script async src="//platform.twitter.com/widgets.js" charset="utf-8"></script>
    </div>

</div>
<div class="top-container-inner top-right">
    <ul>
        <li><a href="https://www.facebook.com/TheChineseBuffet/" target="_blank" class="social-text"><i class="fa fa-facebook" aria-hidden="true"></i></a></li>
        <li><a href="https://twitter.com/TChineseBuffet" target="_blank" class="social-text"><i class="fa fa-twitter" aria-hidden="true"></i></a></li>
        <li class="last"><a href="https://www.instagram.com/the_chinese_buffet/" target="_blank" class="social-text"><i class="fa fa-instagram" aria-hidden="true"></i></a></li>
        <!--<li class="last"><a href="#" target="_self" class="social-text"><i class="fa fa-tripadvisor" aria-hidden="true"></i></a></li>-->
    </ul>
</div>
