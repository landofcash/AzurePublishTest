<template>
    <div class="form-group" v-bind:class="{'has-error': errors.has('input'+_uid) }">
        <label :for="'input'+_uid" class="col-sm-4 control-label">{{name}}</label>
        <div class="col-sm-8">
            <div v-if="required" class="input-group">
                <select :value="valueString" v-on:input="updateValue($event.target.value)" v-validate="'required'" class="form-control" ref="input" :id="'input'+_uid" :name="'input'+_uid">
                    <option v-for="option in options" v-bind:value="option.Value">
                        {{ option.Text }}
                    </option>
                </select>
                <span class="input-group-addon" id="basic-addon1"><span class="glyphicon glyphicon-asterisk" title="Required Field" aria-hidden="true"></span></span>
            </div>
            <select v-if="!required" :value="valueString" v-on:input="updateValue($event.target.value)" class="form-control" ref="input" :id="'input'+_uid" :name="'input'+_uid">
                <option value="">--- Selected {{name}} ---</option>
                <option v-for="option in options" v-bind:value="option.Value">{{ option.Text }}</option>
            </select>
            <span id="helpBlock" class="help-block" v-show="isValid">{{name}} is required.</span>
        </div>
    </div>
</template>

<script>    
    module.exports = 
        {
            inject: ['$validator'],            
            props: {
                name: {
                    type: String,
                    required: true
                },
                value: {
                    type: Number
                },
                options: {
                    type: Array,
                    default: []
                },
                required: {
                    type: Boolean,
                    default: false
                }
            },
            computed: {
                isValid: function () {
                    return this.errors.has('input' + this._uid);
                },
                valueString: function () {
                    return this.value || '';
                }
            },
            methods: {
                updateValue: function (value) {
                    var formattedValue = parseInt(value.trim());
                    this.$emit('input', formattedValue);
                }
            }
        }
</script>