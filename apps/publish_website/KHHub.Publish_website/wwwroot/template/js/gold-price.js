(function () {
    var amountInput = document.querySelector('[data-gold-amount]');
    var unitSelect = document.querySelector('[data-gold-unit]');
    var priceSelect = document.querySelector('[data-gold-price-source]');
    var result = document.querySelector('[data-gold-result]');

    if (!amountInput || !unitSelect || !priceSelect || !result) {
        return;
    }

    var gramsPerUnit = {
        luong: 37.5,
        chi: 3.75,
        gram: 1
    };

    var formatCurrency = function (value) {
        return new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND',
            maximumFractionDigits: 0
        }).format(value);
    };

    var calculate = function () {
        var amount = Number(amountInput.value) || 0;
        var unit = unitSelect.value;
        var pricePerLuong = Number(priceSelect.value) * 1000;
        var grams = amount * (gramsPerUnit[unit] || gramsPerUnit.luong);
        var value = (grams / gramsPerUnit.luong) * pricePerLuong;

        result.textContent = formatCurrency(value);
    };

    [amountInput, unitSelect, priceSelect].forEach(function (element) {
        element.addEventListener('input', calculate);
        element.addEventListener('change', calculate);
    });

    calculate();
})();

