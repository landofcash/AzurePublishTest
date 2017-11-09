<template>
    <div class="form-group" v-bind:class="{'has-error': errors.has('input'+_uid) }">
        <label :for="'input'+_uid" class="col-sm-4 control-label">{{name}}</label>
        <div class="col-sm-8">
            <div class="input-group" v-if="required">
                <textarea v-validate="validateRules" :data-vv-as="name" class="form-control" type="text" ref="input" v-bind:style="textAreaStyle" :id="'input'+_uid" :name="'input'+_uid" :placeholder="name" :value="value" v-on:input="updateValue($event.target.value)"></textarea>
                <span class="input-group-addon" id="basic-addon1"><span class="glyphicon glyphicon-asterisk" title="Required Field" aria-hidden="true"></span></span>
            </div>
            <textarea v-validate="validateRules" :data-vv-as="name" class="form-control" type="text" ref="input" v-bind:style="textAreaStyle" :id="'input'+_uid" :name="'input'+_uid" :placeholder="name" :value="value" v-on:input="updateValue($event.target.value)" v-if="!required"></textarea>
            <span id="helpBlock" class="help-block" v-for="err in errors.errors.filter(function(err){return err.field==='input'+_uid;})">{{err.msg}}</span>
        </div>
    </div>
</template>

<script>
    module.exports = {
        inject: ['$validator'],
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
            },
            email: {
                type: Boolean,
                default: false
            },
            textAreaStyle: {
                type: Object,
                default: function () {
                    return {};
                }
            }
        },
        computed: {
            validateRules: function () {
                var res = {};
                if (this.required) {
                    res.required = true;
                }
                if (this.email) {
                    res.email = true;
                }
                return res;
            }
        },
        methods: {
            updateValue: function (value) {
                var formattedValue = value.trim();
                this.$emit('input', formattedValue);
            }
        }
    }
</script>