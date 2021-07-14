// Copyright (c) Johnson Matthey Organization 2021. All rights reserved.

namespace JM.Integration.Common
{
    public interface ITransform<TInput, TOutput>
    {
        /// <summary>
        /// TranformMarkStructureToMarkStructureDto.
        /// </summary>
        /// <param name="Transform"></param>
        /// <returns>Task.</returns>
        TOutput Transform(TInput input);
    }
}