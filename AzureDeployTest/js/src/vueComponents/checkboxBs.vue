<template>
    <div class="form-group">
        <div class="checkbox col-sm-offset-4">
            <label><input type="checkbox" :id="'input'+_uid" :name="'input'+_uid" :checked="value" v-on:change="updateValue($event.target.checked)"><span>{{name}}</span></label>
        </div>
    </div>
</template>

<script>
    module.exports = 
        {
            template: '#checkboxBs',
            props: {
                name: {
                    type: String,
                    required: true
                },
                value: {
                    type: Boolean,
                    default: false
                }
            },
            methods: {
                updateValue: function (value) {
                    this.$emit('input', value);
                }
            }
        }
</script>