<template>
    <div class="form-group" v-bind:class="{'has-error': errors.has('input'+_uid) }">
        <label :for="'input'+_uid" class="col-sm-4 control-label">{{name}}</label>
        <div class="col-sm-8">
            <div class="input-group" v-if="required">                
                <masked-input  v-validate="validateRules" :data-vv-name="'input'+_uid" :data-vv-as="name" class="form-control" type="text" 
                              :mask="['(', /[1-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/]"
                              :guide="true"
                              placeholderChar="_"
                              :keepCharPositions="true"                             
                              ref="input" :id="'input'+_uid" :name="'input'+_uid" :placeholder="name" :value="value" v-on:input="updateValue($event)">
                </masked-input>
                <span class="input-group-addon" id="basic-addon1"><span class="glyphicon glyphicon-asterisk" title="Required Field" aria-hidden="true"></span></span>
            </div>            
            <masked-input  v-validate="validateRules" :data-vv-name="'input'+_uid" :data-vv-as="name" class="form-control" type="text"
                          :mask="['(', /[1-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/]"
                          :guide="true"
                          placeholderChar="_"
                          :keepCharPositions="true"
                          ref="input" :id="'input'+_uid" :name="'input'+_uid" :placeholder="name" :value="value" v-on:input="updateValue($event)" v-if="!required">
            </masked-input>
            <span id="helpBlock" class="help-block" v-for="err in errors.items.filter(function(err){return err.field==='input'+_uid;})">{{err.msg}}</span>            
        </div>
    </div>
</template>

<script>
    var maskedInput = require('vue-text-mask');
    module.exports = {
        inject: ['$validator'],
        components: {
            maskedInput: maskedInput.default
        },
        props: {
            name: {
                type: String,
                required: true
            },
            value: {
                type: String,
                default: ''
            },
            required: {
                type: Boolean,
                default: false
            }
        },       
        computed: {
            validateRules: function () {
                var res = {};
                if (this.required) {
                    res.required = true;
                }                
                return res;
            }
        },
        methods: {
            updateValue: function (value) {
                console.log("input event:" + value);
                var formattedValue = value.trim();
                this.$emit('input', formattedValue);
            }
        }
    }
</script>