<?PHP
	$mailMsg = '';
    $email = $_POST['email'];
	$nameTelAddress = $_POST['nameTelAddress'];	
    $history = $_POST['history'];	
    $experience = $_POST['experience'];	
    $achievements = $_POST['achievements'];	
    $profile = $_POST['profile'];	
    $education = $_POST['education'];	
    $references = $_POST['references'];

    $message = "";
	$headers = "MIME-Version: 1.0\r\n";
	$headers .= "Content-Type: text/html; charset=ISO-8859-1\r\n";
	$message .= "<html><body>";
	$message .= "<span style='font-family:Verdana, Geneva, sans-serif; font-size:12px; color:#333;'>";
    $message .= "<b>Email :</b> ".$email."<br/><br/>";	
    $message .= "<b>Name, Tel, Address :</b> ".$nameTelAddress."<br/><br/>";	
    $message .= "<b>Career History :</b> ".$history."<br/><br/>";	
    $message .= "<b>Experience :</b> ".$experience."<br/><br/>";	
    $message .= "<b>Achievements :</b> ".$achievements."<br/><br/>";	
    $message .= "<b>Personal Profile :</b> ".$profile."<br/><br/>";	
    $message .= "<b>Education :</b> ".$education."<br/><br/>";	
    $message .= "<b>References :</b> ".$references."<br/><br/>";	
	$message .= "</span>";
	$message .= "</body></html>";

	$to = "mohd.shanu@gmail.com, ronak@nativeasia.com";
	$subject = "The Chinese Buffet, Careers";
	$from = "enquiry@nativeasia.com";
	$headers .= "From:" . $from;
	mail($to,$subject, $message, $headers);
	echo 'Thank you...!';;

?>
