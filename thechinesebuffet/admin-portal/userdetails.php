<?PHP
    session_start(); 
    
    if(isset($_SESSION['username']) && $_SESSION['username'] != ''){
        $username = $_SESSION['username'];
        $profilePic = $_SESSION['profilepic'];
    }else{
        echo '<script> window.open("login.php", "_self"); </script>';
    }
?>
