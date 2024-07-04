namespace Assignment.Services.Account
{
    using DataAccess.Interfaces;
    using Exceptions;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Class with account interaction logic
    /// </summary>
    public class AccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        /// <summary>
        /// Transfers money from source account to destination account.
        /// In error throws exception
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="destinationId"></param>
        /// <param name="amount"></param>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <exception cref="NotEnoughMoneyException"></exception>
        public async Task Transfer(Guid sourceId, Guid destinationId, decimal amount)
        {
            var account = await _unitOfWork.AccountRepository.GetById(sourceId);
            var destination = await _unitOfWork.AccountRepository.GetById(destinationId);
            
            if (account == null)
            {
                throw new AccountDoesNotExistException(sourceId);
            }
            
            if (destination == null)
            {
                throw new AccountDoesNotExistException(destinationId);
            }

            if (account.Amount < amount)
            {
                throw new NotEnoughMoneyException(sourceId);
            }
            
            account.Amount -= amount;
            destination.Amount += amount;

            await _unitOfWork.SaveChangesAsync(default);
        }
    }
}
