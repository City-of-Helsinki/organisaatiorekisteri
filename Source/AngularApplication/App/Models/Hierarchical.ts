﻿"use strict";

module ServiceRegister
{
    export class Hierarchical implements Affecto.Base.IModel
    {
        constructor(public id: string, public name: string, public children: Array<Hierarchical>)
        {
        }
    }
}   