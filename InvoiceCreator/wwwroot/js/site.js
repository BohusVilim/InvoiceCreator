//otvori modalne okno po kliknuti na "kontaktuj ma!"
$(".contact-item, .contact-item2, .contact-button").on("click", function () {
    $(".popup-overlay, .popup-content, .popup-bg").addClass("active");
});

//zatvori modalne okno po kliknti na tlacitko close
$(".close").on("click", function () {
    $(".popup-overlay, .popup-content, .popup-bg").removeClass("active");
});