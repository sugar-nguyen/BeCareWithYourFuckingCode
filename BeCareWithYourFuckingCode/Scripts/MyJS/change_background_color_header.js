$(function(){
	$(".navbar-toggler").mousedown(function(){
		var $p = $(".header");
		var $black = "black";
		var $color = $p.css("background-color");
		$color = $color.toString();
		if($color == "rgba(0, 0, 0, 0.6)"){
			$p.css("background-color","black");
		}
		else{
			$p.css("background-color","rgba(0, 0, 0, 0.6)");
		}
	});
});