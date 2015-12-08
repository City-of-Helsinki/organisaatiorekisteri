﻿using System.Collections.Generic;

namespace ServiceRegister.Application
{
    public interface IHierarchical
    {
        bool HasParent { get; }
        IEnumerable<IHierarchical> Children { get; }
        bool IsMyChild(IHierarchical hierarchical);
        void AddChildren(IEnumerable<IHierarchical> items);
    }
}
