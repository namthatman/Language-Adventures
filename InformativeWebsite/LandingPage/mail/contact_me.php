<?php
// Check for empty fields
if(empty($_POST['name']) || empty($_POST['email']) || !filter_var($_POST['email'],FILTER_VALIDATE_EMAIL))
   {
      http_response_code(500);
      exit();
   }
   
$name = strip_tags(htmlspecialchars($_POST['name']));
$email = strip_tags(htmlspecialchars($_POST['email']));
$message = strip_tags(htmlspecialchars($_POST['message']));
$time = new DateTime();
$time_format = $time->format('Y-m-d');
 
// Write to text file.

$my_file = "../database.txt";
$handle = fopen($my_file, 'a') or die('Cannot open file:  '.$my_file);

$txt = $name.'#'.$email.'#'.$message.'#'.$time_format;
fwrite($handle, $txt."\n");
fclose($handle);        
?>