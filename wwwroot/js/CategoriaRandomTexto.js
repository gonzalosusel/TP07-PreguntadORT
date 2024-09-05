function CrearTextoRandom(Categoria, Pos, EsElReal = false){
    tempEl = document.createElement("p");
    tempEl.classList.add(EsElReal ? "categoria-elegida-ruleta" : "categoria-ruleta");
    tempEl.textContent = Categoria;

    tempEl.style.top = window.innerHeight / 2 + "px";
    tempEl.style.left = Pos * 100 + "px";
    document.body.appendChild(tempEl);
}