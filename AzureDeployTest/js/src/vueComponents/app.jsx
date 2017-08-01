var IMSVueComponents = {};

var addressSuggestBs = require('./addressSuggestBs.vue');
var checkboxBs = require('./checkboxBs.vue');
var inputBs = require('./inputBs.vue');
var passwordBs = require('./passwordBs.vue');
var selectBs = require('./selectBs.vue');
var selectStringBs = require('./selectStringBs.vue');
var typeahead = require('./typeahead.vue');

IMSVueComponents.addressSuggestBs = addressSuggestBs;
IMSVueComponents.checkboxBs = checkboxBs;
IMSVueComponents.inputBs = inputBs;
IMSVueComponents.passwordBs = passwordBs;
IMSVueComponents.selectBs = selectBs;
IMSVueComponents.selectStringBs = selectStringBs;
IMSVueComponents.typeahead = typeahead;


window.IMSVueComponents = IMSVueComponents;
Vue.use(VeeValidate);