<?PHP
	$mailMsg = '';
	$location = $_POST['getlocation'];
	$date = $_POST['date'];
    $guest = $_POST['getguest'];
    $slot = $_POST['getslot'];
	$name = $_POST['name'];
    $mobile = $_POST['mobile'];
    $email = $_POST['email'];
	$comment = $_POST['comment'];	
	$message = "";
	$headers = "MIME-Version: 1.0\r\n";
	$headers .= "Content-Type: text/html; charset=ISO-8859-1\r\n";
	$message .= "<html><body>";
	$message .= "<span style='font-family:Verdana, Geneva, sans-serif; font-size:12px; color:#333;'>";
    $message .= "<b>Location :</b> ".$location."<br/><br/>";
    $message .= "<b>Date :</b> ".$date."<br/><br/>";
	$message .= "<b>No. of guets :</b> ".$guest."<br/><br/>";
	$message .= "<b>Time :</b> ".$slot."<br/><br/>";
	$message .= "<b>Name :</b> ".$name."<br/><br/>";
    $message .= "<b>Mobile :</b> ".$mobile."<br/><br/>";
    $message .= "<b>Email :</b> ".$email."<br/><br/>";	
	$message .= "<b>Message :</b> ".$comment."<br/><br/>";
	$message .= "</span>";
	$message .= "</body></html>";
	$to = "mohd.shanu@gmail.com, ronak@nativeasia.com";
	$subject = "The Chinese Buffet, Table Booking";
	$from = "enquiry@nativeasia.com";
	$headers .= "From:" . $from;
	mail($to,$subject, $message, $headers);
	echo 'Thank you for making a reservation at THE Chinese Buffet! Your reservation code is: 0000';;

?>
