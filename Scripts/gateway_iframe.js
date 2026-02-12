function displayPG(url) {
    debugger;
    var concerto_checkout = document.createElement("concerto_checkout");

    var concerto_checkout_iframe = document.createElement("iframe");
    concerto_checkout_iframe.setAttribute("id", "concerto_checkout_iframe");
    concerto_checkout_iframe.setAttribute("src", url);
    concerto_checkout_iframe.setAttribute("scrolling", "no");
    concerto_checkout_iframe.setAttribute("title", "Payment Gateway");
    concerto_checkout_iframe.setAttribute("width", "500");
    concerto_checkout_iframe.setAttribute("height", "520");
    concerto_checkout_iframe.setAttribute("style", "border:1px solid #DDD; 			background:white; margin-left:30%; margin-top:4%;");
    concerto_checkout.appendChild(concerto_checkout_iframe);


}