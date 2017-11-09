var IMSVueComponents = {};

var addressSuggestBs = require('./addressSuggestBs.vue');
var checkboxBs = require('./checkboxBs.vue');
var inputBs = require('./inputBs.vue');
var passwordBs = require('./passwordBs.vue');
var selectBs = require('./selectBs.vue');
var selectStringBs = require('./selectStringBs.vue');
var typeahead = require('./typeahead.vue');
var textAreaBs = require('./textAreaBs.vue');
var datePicker = require('./datePicker.vue');
var phoneBs = require('./phoneBs.vue');
var maskedInput = require('vue-text-mask');
IMSVueComponents.addressSuggestBs = addressSuggestBs;
IMSVueComponents.checkboxBs = checkboxBs;
IMSVueComponents.inputBs = inputBs;
IMSVueComponents.passwordBs = passwordBs;
IMSVueComponents.selectBs = selectBs;
IMSVueComponents.selectStringBs = selectStringBs;
IMSVueComponents.typeahead = typeahead;
IMSVueComponents.textAreaBs = textAreaBs;
IMSVueComponents.phoneBs = phoneBs;
IMSVueComponents.datePicker = datePicker;
IMSVueComponents.maskedInput = maskedInput.default;


window.IMSVueComponents = IMSVueComponents;
Vue.use(VeeValidate);
//Vue.component('masked-input', MaskedInput);