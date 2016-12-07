var homeMessage = ["Your favorite recipes...without killing trees",
    "Keep all Grandma's recipes in one place!",
    "For those times when you need a recipe for boiling water."
]

var getRandomNumberBetween = function(min, max){
    number = Math.floor(Math.random() * (max - min) + min);
    return number;
}

$(document).ready(function () {
    $("#home-message").text(homeMessage[getRandomNumberBetween(0, homeMessage.length)]);
    $("#random-num-val").text(getRandomNumberBetween(0, 3));
}); 