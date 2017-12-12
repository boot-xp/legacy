var apiUrl = 'http://localhost:5000/api';

var CustomersService = (function ($) {
    function getOrders(customerId) {
        return $.getJSON(apiUrl + '/customers/' + customerId + '/orders');
    }
    
    return function () {
        return { getOrders: getOrders };
    }
})($);

var ProductsService = (function ($) {
    function getProducts() {
        return $.getJSON(apiUrl + '/products');
    }
    
    function getProduct(id) {
        return $.getJSON(apiUrl + '/products/' + id);
    }
    
    return function () {
        return { getProducts: getProducts, getProduct: getProduct };
    }
})($);

var OrdersService = (function ($) {
    function createOrder(order) {
        return $.ajax({
            type: 'POST',
            url: apiUrl + '/orders', 
            data: JSON.stringify(order),
            contentType: 'application/json'
        });
    }
    
    return function () {
        return { createOrder: createOrder };
    }
})($);