function updateCountdown(){
    const NOW = new Date()
    const DURATION = DATE_TARGET - NOW;
    const REMAINING_MINUTES = Math.floor((DURATION % 3600000) / 60000);
    const REMAINING_SECONDS = Math.floor((DURATION % 60000) / 1000);

    if(REMAINING_SECONDS == 0 && REMAINING_MINUTES == 0){
        document.querySelector('span#minutes').textContent = "00";
        document.querySelector('span#seconds').textContent = "00";
    } else {
        if(REMAINING_MINUTES.toString().length == 1){
            document.querySelector('span#minutes').textContent = "0" + REMAINING_MINUTES.toString();
        } else {
            document.querySelector('span#minutes').textContent = REMAINING_MINUTES.toString();
        }
        
        if(REMAINING_SECONDS.toString().length == 1){
            document.querySelector('span#seconds').textContent = "0" + REMAINING_SECONDS.toString();
        } else {
            document.querySelector('span#seconds').textContent = REMAINING_SECONDS.toString();
        }
    }

    document.getElementById("TiempoRestante").value = Math.floor((DURATION % 3600000)) + Math.floor((DURATION % 60000) / 1000);
}