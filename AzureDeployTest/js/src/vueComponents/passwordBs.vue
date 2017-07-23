<template>
    <div>
        <div class="form-group" v-bind:class="{'has-error': errors.has('input'+_uid) }">
            <label :for="'input'+_uid" class="col-sm-4 control-label">{{name}}</label>
            <div class="col-sm-8">
                <input v-validate="'required'" :data-vv-as="name" class="form-control" type="password" ref="input" :id="'input'+_uid" :name="'input'+_uid" :placeholder="name" :value="value" v-on:input="updateValue($event.target.value)">
                <span id="helpBlock" class="help-block" v-for="err in errors.errors.filter(function(err){return err.field===pwdInputName;})">{{err.msg}}</span>
            </div>
        </div>
        <div class="form-group" v-bind:class="{'has-error': errors.has('pwd_confirm'+_uid) }">
            <label :for="'input'+_uid" class="col-sm-4 control-label">Confirm {{name}}</label>
            <div class="col-sm-8">
                <input v-validate="{required:true, confirmed:pwdInputName}" :data-vv-as="'Confirm '+name" :id="pwdInputConfirmName" :name="pwdInputConfirmName" type="password" class="form-control" placeholder="Password, Again" data-vv-validate-on="blur">
                <span id="helpBlock" class="help-block" v-for="err in errors.errors.filter(function(err){return err.field===pwdInputConfirmName;})">{{err.msg}}</span>
            </div>
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
            }
        },
        computed: {
            pwdInputName: function () {
                return 'input' + this._uid;
            },
            pwdInputConfirmName: function () {
                return 'pwd_confirm' + this._uid;
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